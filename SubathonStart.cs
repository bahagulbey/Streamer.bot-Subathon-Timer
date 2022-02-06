using System;
using System.Timers;

class CPHInline
{
    public System.Timers.Timer countdownTimer;
    public int subathonSecondsLeft;
    public int subathontotalTimeInSeconds;
    public int subathonCapInSeconds;
    public void Init()
    {
        countdownTimer = new System.Timers.Timer(1000);
        countdownTimer.Elapsed += OnTimedEvent;
        countdownTimer.AutoReset = true;
        countdownTimer.Enabled = true;
        countdownTimer.Stop();
    }

    public bool Execute()
    {
        // Change maxHourValue to max length of stream in hours
        int maxHourValue = 24;
        subathonCapInSeconds = maxHourValue * (3600);
        // Change hourValue to initial length of stream in hours
        int hourValue = 3;
        subathonSecondsLeft = hourValue * (3600) + 1;
        subathontotalTimeInSeconds = subathonSecondsLeft;
        countdownTimer.Start();
        return true;
    }

    public void OnTimedEvent(Object source, ElapsedEventArgs e)
    {
        subathonSecondsLeft--;
        TimeSpan time = TimeSpan.FromSeconds(subathonSecondsLeft);
        string countdownString = time.ToString(@"hh\:mm\:ss");
        if (subathonSecondsLeft == 0)
        {
            StopTimer("All done!");
			CPH.RunAction("SubathonDone");
        }
        else
        {
            // Set to scene and source of your text source
            CPH.ObsSetGdiText("[NS] SubathonTimer", "[TS] SubathonCounter", countdownString);
        }
    }

    public void Dispose()
    {
        countdownTimer.Dispose();
    }

    private void StopTimer(string message)
    {
        // Set to Scene and Source of your text source
        CPH.ObsSetGdiText("[NS] SubathonTimer", "[TS] SubathonCounter", message);
        countdownTimer.Stop();
    }

    private void AddMinutes(int minutesToAdd)
    {
        int secondsToAdd = minutesToAdd * 60;
        if ((subathontotalTimeInSeconds + secondsToAdd) < subathonCapInSeconds)
        {
            subathontotalTimeInSeconds = subathontotalTimeInSeconds + secondsToAdd;
            subathonSecondsLeft = subathonSecondsLeft + secondsToAdd;
        }
        else
        {
            subathonSecondsLeft = subathonSecondsLeft + (subathonCapInSeconds - subathontotalTimeInSeconds);
            subathontotalTimeInSeconds = subathonCapInSeconds;
            CPH.SendMessage("We've reached the sub-a-thon limit! No more time will be added.", true);
        }
    }

    public bool Stop()
    {
        StopTimer("Sub-a-thon cancelled!");
        return true;
    }

    public bool Tier1()
    {
        // Change minuteValue to minutes to add to the timer per Tier 1 sub
        int minuteValue = 2;
        AddMinutes(minuteValue);
        return true;
    }

    public bool Tier2()
    {
        // Change minuteValue to minutes to add to the timer per Tier 2 sub
        int minuteValue = 5;
        AddMinutes(minuteValue);
        return true;
    }

    public bool Tier3()
    {
        // Change minuteValue to minutes to add to the timer per Tier 3 sub
        int minuteValue = 12;
        AddMinutes(minuteValue);
        return true;
    }
}
