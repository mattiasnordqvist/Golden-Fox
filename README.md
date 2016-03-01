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

This fox ain't golden for no reason. As long as the words you talk fit into [this grammar](GoldenFox.ANTLR/GoldenFoxLanguage.g4), you're fine.

## The Api

```csharp

Schedule.Fox("every day @ 10:00").From(DateTime.Today);

```

or

```csharp

new Fox("every day @ 10:00", DateTime.Today);

```
Returns an IEnumerable starting with the next occurence from the given date (inclusive)

If you scratch the surface of the golden fox, you'll soon see that his fur is just a disguise. The bare bones of this creature is just a compiler which you can access directly like this


```csharp

Fox.Compile("every day @ 10:00").Evaluate(DateTime.Today)

```

which will return just the first occurence from the given date. Not inclusively this time though. Consider 


```csharp

Fox.Compile("every day @ 10:00").Evaluate(DateTime.Today, true)

```

if you want inclusiveness.

## Contribute

It is hard to teach foxes English. You're welcome to give him a lesson and expand his vocabulary and understanding. Or just create an issues for input that you wish the fox to recognize.

Make a pull request and give me a lesson in parsing. I need it...

