# Golden-Fox
Foxy scheduling

I have always found scheduling messy. I mean, how do you even store [this](http://www.codeproject.com/KB/Tools-IDE/376731/JobScheduleProperties1.jpg) in a database? Imagine you could just say do this and that on the "2nd last day every month at 22:30"? Well, no need to imagine. Now you can!

## Yes, we can!

```csharp

Schedule.Fox("every day @ 10:00").From(DateTime.Now).First()  // => Would give you a datetime representing today or tomorrow at 10:00, depending on what the time is now.
Schedule.Fox("every day @ 10:00").From(DateTime.Now).Take(10)  // => An IEnumerable<DateTime> with 10 entries starting from today or tomorrow, depending on what the time is now.

```

With a little creativity you could design your own extension method so that this syntax is available for you
```csharp

"sundays at 22:30".From("2015-08-14").First() // => 2015-08-16 22:30:00

```

See how simple this schedule is to store in a database? You need ONE column of one of the simplest types. No modeling required. You can now even store it in a configuration file wihtout hassle. The format is also easily recognized and understood by other programmers or users. 

A typical situation when Golden Fox comes to use is when you are scheduling services to run at specific times. 
Given a schedule and a point in time, you can let Golden Fox find a future point in time according to the schedule.

## Does Golden Fox understand English?

This fox ain't golden for no reason. As long as the words you talk fit into this flow chart below, you're fine.

![Flow chart](https://raw.githubusercontent.com/mattiasnordqvist/Golden-Fox/master/diagram.png "Simple english")

## Contribute

It is hard to teach foxes English. You're welcome to give him a lesson and expand his vocabulary and understanding. :D

I have really no idea about how you should write code to do this kind of task in a nice way. This is my humble first try. Please advice me or make a pull request where you fix the mess behind the scenes.
