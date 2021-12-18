using System;
using System.Timers;

class CPHInline
{
    public System.Timers.Timer countdownTimer;
    public int secondsLeft;
    public int totalTimeInSeconds;
    public int maxTotalTimeInSeconds;
    public void Init()
    {
        countdownTimer = new System.Timers.Timer(1000);
        countdownTimer.Elapsed += OnTimedEvent;
        countdownTimer.AutoReset = true;
        countdownTimer.Enabled = true;
        countdownTimer.Stop();
    }

    public void Dispose()
    {
        countdownTimer.Dispose();
    }

    public void OnTimedEvent(Object source, ElapsedEventArgs e)
    {
        secondsLeft--;
        TimeSpan time = TimeSpan.FromSeconds(secondsLeft);
        string countdownString = time.ToString(@"hh\:mm\:ss");
        if (secondsLeft == 0)
        {
            // Set to Scene and Source of your text source and a message once timer hits zero
            CPH.ObsSetGdiText("SubathonTimer", "SubathonCounter", "All done!");
            countdownTimer.Stop();
        }
        else
        {
            // Set to Scene and Source of your text source
            CPH.ObsSetGdiText("SubathonTimer", "SubathonCounter", countdownString);
        }
    }

    public bool Execute()
    {
        // Change hourValue to max length of stream in hours
        int maxHourValue = 2;
        maxTotalTimeInSeconds = maxHourValue * (3600);
        // Change hourValue to initial length of stream in hours
        int hourValue = 1;
        secondsLeft = hourValue * (3600) + 1;
        totalTimeInSeconds = secondsLeft;
        countdownTimer.Start();
        return true;
    }
	
    public bool Stop()
    {
        // Set to Scene and Source of your text source and a message once timer hits zero
        CPH.ObsSetGdiText("SubathonTimer", "SubathonCounter", "Timer cancelled!");
        countdownTimer.Stop();
		return true;
    }

    public bool Tier1()
    {
        // Change minuteValue to minutes to add to the timer per Tier 1 sub
        int minuteValue = 2;
        int secondsToAdd = minuteValue * 60;
        if ((totalTimeInSeconds + secondsToAdd) < maxTotalTimeInSeconds)
        {
            totalTimeInSeconds = totalTimeInSeconds + secondsToAdd;
            secondsLeft = secondsLeft + secondsToAdd;
        }
        else
        {
            secondsLeft = secondsLeft + (maxTotalTimeInSeconds - totalTimeInSeconds);
            totalTimeInSeconds = maxTotalTimeInSeconds;
			CPH.SendMessage("We've reached the subathon limit! No more time will be added.",true);
        }
        return true;
    }

    public bool Tier2()
    {
        // Change minuteValue to minutes to add to the timer per Tier 2 sub
        int minuteValue = 5;
        int secondsToAdd = minuteValue * 60;
        if ((totalTimeInSeconds + secondsToAdd) < maxTotalTimeInSeconds)
        {
            totalTimeInSeconds = totalTimeInSeconds + secondsToAdd;
            secondsLeft = secondsLeft + secondsToAdd;
        }
        else
        {
            secondsLeft = secondsLeft + (maxTotalTimeInSeconds - totalTimeInSeconds);
            totalTimeInSeconds = maxTotalTimeInSeconds;
			CPH.SendMessage("We've reached the subathon limit! No more time will be added.",true);
        }
        return true;
    }

    public bool Tier3()
    {
        // Change minuteValue to minutes to add to the timer per Tier 3 sub
        int minuteValue = 12;
        int secondsToAdd = minuteValue * 60;
        if ((totalTimeInSeconds + secondsToAdd) < maxTotalTimeInSeconds)
        {
            totalTimeInSeconds = totalTimeInSeconds + secondsToAdd;
            secondsLeft = secondsLeft + secondsToAdd;
        }
        else
        {
            secondsLeft = secondsLeft + (maxTotalTimeInSeconds - totalTimeInSeconds);
            totalTimeInSeconds = maxTotalTimeInSeconds;
			CPH.SendMessage("We've reached the subathon limit! No more time will be added.",true);

        }
        return true;
    }
}
