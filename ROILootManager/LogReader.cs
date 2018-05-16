using System;
using System.IO;
using log4net;
using System.Threading;
using System.Text.RegularExpressions;

namespace ROILootManager
{
  // A delegate type for hooking up change notifications.
  public delegate void LogReaderEvent(object sender, LogEventArgs e);

  public class LogReader
  {
    private static ILog logger = LogManager.GetLogger(typeof(LogReader));

    private string logFileName = "eqlog_Kizant_xegony2.txt";
    private string logFilePath = "C:\\Users\\Public\\Sony Online Entertainment\\Installed Games\\EverQuest\\Logs-Saved\\";

    private Thread myThread;
    private ThreadState threadState;

    public enum logTypes { GUILD_CHAT, OFFICER_CHAT, TELLS, LOOT };
    private Regex guildChat = new Regex(@"\[.+\] \w+ (tells the guild|say to your guild)");
    private Regex officerChat = new Regex(@"\[.+\] \w+ tell?(\w) (?i)officersofroi(?-i):");
    private Regex tells = new Regex(@"\[.+\] \w+ tells you, '");
    private Regex lootedItem = new Regex(@"\[.+\] --(\w+) (has|have) looted a (.+)\.--");
    private Regex userFromFileName = new Regex(@"eqlog_(\w+)_");

    public event LogReaderEvent logEvent;
    public LogReader()
    {
    }

    public bool setLogFile(string file)
    {
      if (File.Exists(file))
      {
        logFilePath = file.Substring(0, file.LastIndexOf("\\")) + "\\";
        logFileName = file.Substring(file.LastIndexOf("\\") + 1);
        PropertyManager.getManager().setProperty("EQlogFile", file);

        // parse current user name
        MatchCollection matches = userFromFileName.Matches(logFileName);
        if (matches.Count > 0)
        {
          PropertyManager.getManager().setProperty("UserName", matches[0].Groups[1].Value);
        }

        return true;
      }

      return false;
    }

    public void start()
    {
      if (threadState != null)
      {
        threadState.stop();
      }

      threadState = new ThreadState();
      ThreadState myState = threadState;

      myThread = new Thread(() =>
      {
        Boolean exitOnError = false;

        // get file stream
        FileStream fs = new FileStream(logFilePath + logFileName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
        StreamReader reader = new StreamReader(fs);

        // setup watcher
        FileSystemWatcher fsw = new FileSystemWatcher
        {
          Path = logFilePath,
          Filter = logFileName
        };

        // events to notify for changes
        fsw.NotifyFilter = NotifyFilters.LastAccess | NotifyFilters.LastWrite | NotifyFilters.CreationTime;

        // read to end and start listening for events
        reader.ReadToEnd();
        fsw.EnableRaisingEvents = true;

        while (myState.isRunning() && !exitOnError)
        {
          WaitForChangedResult result = fsw.WaitForChanged(WatcherChangeTypes.Deleted | WatcherChangeTypes.Changed, 2000);

          // check if exit during wait period
          if (!myState.isRunning() || exitOnError)
          {
            break;
          }

          switch (result.ChangeType)
          {
            case WatcherChangeTypes.Deleted:
              // file gone
              exitOnError = true;
              break;
            case WatcherChangeTypes.Changed:
              if (reader != null)
              {
                while (!reader.EndOfStream)
                {
                  string line = reader.ReadLine();
                  if (logEvent != null && line.Length > 0)
                  {
                    MatchCollection matches;
                    if ((matches = officerChat.Matches(line)).Count > 0)
                    {
                      logEvent(this, new LogEventArgs(line, matches, LogReader.logTypes.OFFICER_CHAT));
                    }
                    else if ((matches = guildChat.Matches(line)).Count > 0)
                    {
                      logEvent(this, new LogEventArgs(line, matches, LogReader.logTypes.GUILD_CHAT));
                    }
                    else if ((matches = tells.Matches(line)).Count > 0)
                    {
                      logEvent(this, new LogEventArgs(line, matches, LogReader.logTypes.TELLS));
                    } else if ((matches = lootedItem.Matches(line)).Count > 0)
                    {
                      logEvent(this, new LogEventArgs(line, matches, LogReader.logTypes.LOOT));
                    }
                  }
                }
              }
              break;
          }
        }

        if (reader != null)
        {
          reader.Close();
        }

        if (fs != null)
        {
          fs.Close();
        }

        if (fsw != null)
        {
          fsw.Dispose();
        }

      });
      
      myThread.Start();
    }

    public void end()
    {
      if (threadState != null)
      {
        threadState.stop();
      }

      if (myThread != null)
      {
        myThread.Join(3000);
      }
    }
  }

  public class LogEventArgs : EventArgs
  {
    public string line { get; set; }
    public MatchCollection matches { get; set; }
    public LogReader.logTypes type { get; set; }
    public LogEventArgs(string line, MatchCollection matches, LogReader.logTypes type)
    {
      this.line = line;
      this.matches = matches;
      this.type = type;
    }
  }

  public class ThreadState
  {
    private Boolean running = true;

    public void stop()
    {
      running = false;
    }

    public Boolean isRunning()
    {
      return running;
    }
  }
}
