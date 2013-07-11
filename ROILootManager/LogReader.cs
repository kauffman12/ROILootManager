using System;
using System.IO;
using log4net;
using System.Threading;
using System.Text.RegularExpressions;

namespace ROILootManager {
    // A delegate type for hooking up change notifications.
    public delegate void LogReaderEvent(object sender, LogEventArgs e);

    public class LogReader {
        private static ILog logger = LogManager.GetLogger(typeof(LogReader));

        private string logFileName = "eqlog_Corlen_xegony.txt";
        private string logFilePath = "C:\\EverQuest\\Logs\\";

        private Thread t;
        private bool keepReading;

        public enum logTypes { GUILD_CHAT, OFFICER_CHAT };
        private Regex guildChat = new Regex("tells the guild");
        private Regex officerChat = new Regex("officersofroi:");
        //private Regex officerChat = new Regex("guildundertow:");

        public event LogReaderEvent logEvent;

        private FileSystemWatcher fsw;
        private FileStream file;
        StreamReader reader;

        public LogReader() {
            //t = new Thread(ReadFromFile);
        }

        public bool setLogFile(string file) {
            if (File.Exists(file)) {
                logFilePath = file.Substring(0, file.LastIndexOf("\\")) + "\\";
                logFileName = file.Substring(file.LastIndexOf("\\") + 1);
                PropertyManager.getManager().setProperty("EQlogFile", file);
                logger.Info("New log file set " + file);
                return true;
            }

            return false;
        }

        public void start() {
            logger.Debug("Start call made...");
            keepReading = true;

            if (t != null && t.IsAlive) {
                t.Abort();
                cleanUp();
            }

            t = new Thread(ReadFromFile);

            t.Start();
        }

        public void end() {
            logger.Debug("End call made...");
            // TODO this is horrible
            keepReading = false;

            if (t != null)
                t.Abort();
            cleanUp();
        }

        private void ReadFromFile() {
            long offset = 0;

            fsw = new FileSystemWatcher {
                Path = logFilePath,
                Filter = logFileName
            };

            ManualResetEvent workToDo = new ManualResetEvent(false);
            fsw.NotifyFilter = NotifyFilters.LastWrite;
            fsw.Changed += (source, e) => { workToDo.Set(); };
            fsw.Created += (source, e) => { workToDo.Set(); };


            file = File.Open(
               logFilePath + logFileName,
               FileMode.Open,
               FileAccess.Read,
               FileShare.ReadWrite);

            reader = new StreamReader(file);

            // Go to the end of the log and update the offset
            reader.ReadToEnd();
            offset = file.Position;

            fsw.EnableRaisingEvents = true;

            while (keepReading) {
                //fsw.WaitForChanged(WatcherChangeTypes.Changed);
                if (workToDo.WaitOne()) {
                    workToDo.Reset();
                }

                file.Seek(offset, SeekOrigin.Begin);
                if (!reader.EndOfStream) {
                    do {
                        string line = reader.ReadLine();
                        //logger.Debug(line);
                        if (logEvent != null && line.Length > 0) {
                            if (officerChat.IsMatch(line)) {
                                logger.Debug("Adding new Officer chat " + line);
                                logEvent(this, new LogEventArgs(line, LogReader.logTypes.OFFICER_CHAT));
                            } else if (guildChat.IsMatch(line)) {
                                logger.Debug("Adding new Guild Chat " + line);
                                logEvent(this, new LogEventArgs(line, LogReader.logTypes.GUILD_CHAT));
                            }

                        }
                    } while (!reader.EndOfStream);

                    offset = file.Position;
                }
                //Thread.Sleep(1000);
            }

            logger.Debug("Main while loop ended...");

            cleanUp();

        }

        public void cleanUp() {
            logger.Debug("Cleaning up...");
            if (reader != null) {
                reader.Close();
                reader = null;
            }

            if (file != null) {
                file.Close();
                file = null;
            }

            if (fsw != null) {
                fsw.Dispose();
                fsw = null;
            }
        }
    }

    public class LogEventArgs : EventArgs {
        public string line { get; set; }
        public LogReader.logTypes type { get; set; }
        public LogEventArgs(string line, LogReader.logTypes type) {
            this.line = line;
            this.type = type;
        }
    }
}
