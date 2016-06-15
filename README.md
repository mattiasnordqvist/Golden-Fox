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

Another is when you want to generate events at these intervals. It is easy to create an observable from a Golden Fox schedule.

```csharp
var enumerator = Schedule.Fox("every second").From(DateTime.Now).GetEnumerator();
enumerator.MoveNext();

var observable = Observable.Generate(
    enumerator.Current,
    i => true,
    i => 
    {
        enumerator.MoveNext();
        return enumerator.Current;
    },
    i => i,
    i => new TimeSpan(0, 0, 0, (int)Math.Max(0, (i - DateTime.Now).TotalSeconds)));

observable.Subscribe(x => Console.WriteLine(x));
```

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
Returns an `IEnumerable<DateTime` starting with the next occurence from the given date (inclusive)

If you scratch the surface of the golden fox, you'll soon see that his fur is just a disguise. The bare bones of this creature is just a compiler which you can access directly like this


```csharp

Fox.Compile("every day @ 10:00").Evaluate(DateTime.Today)

```

which will return just the first occurence from the given date. Not inclusively this time though. Consider 


```csharp

Fox.Compile("every day @ 10:00").Evaluate(DateTime.Today, true)

```

if you want inclusiveness. However, in this case it wouldn't matter, because DateTime.Today evaluates to Today at 00:00 which is makes 10:00 the next occurence no matter if you include 00:00 or not as a viable option.

## Intervals

Intervals are expressions like `every minute` or `every day @ 10:00`. Intervals can be combined by placing an `and` between them. Like `ever minute and every day @ 10:00`. However, combining these two wouldn't make any sense, because every day @ 10:00 is also a minute, but you get the point. An interval must specify a point in time that is recurring. Available intervals are

### every hour
This means at 00:00, 01:00, 02:00, 03:00 and so forth. You can offset the minutes and seconds by using any of these syntaxes. 
`every hour @ 15 minutes` `every hour @ hh:15`. Offsets can also be combined with `and`, like `every hour @ hh:15 and hh:45`.

### every minute
Like every hour, but for minutes. Offsets can be expressed like `every minute @ 15 seconds`, `every minute @ mm:15` or `every minute @ hh:mm:15`

### every second
Like every hour or every minute, except you can't offset it. Golden Fox don't care about milliseconds. 

### every day
`every day` can not stand alone. You must supply at least one `time`, like `every day at 10:00`. Multiple `time`s can be used. `every day at 10:00 and 18:00`. A time can be expressed like `hh:mm:ss` or `hh:mm`. `13:45` equals `13:45:00`.

### every weekday
`every thursday at 15:00` and `thursdays at 15:00` means the same thing.

### every day in week
`3rd day every week @ 12:30` or `4th last day every week @ 12:30` means the same thing. Any nonsensical input is not guarded against. No one knows what would happen if you say `14th day every week @ 12:30`.

### every day in month
Works the same way as every day in week, except it works with months instead. `last day every month @ 12:30` or `15th day every month @ 12:30` are both valid input. `31st day every month @ 12:30` is not valid input.

### every day in year
Like the above but for years. `last day every year at 12:00`. 

## Constraints
On all of the intervals above, you can add constraints. 

### Between
Can be added to `every hour`, `every minute` and `every second`. It would look like this: `every hour between 14:00 and 18:00`. Constraints are inclusive so generating a sequence of dates from this interval would have `14:00` and `18:00` included.

### From
Can be added to any interval. Defines a starting point for the interval. `every day @ 10:00 from 2016-01-01` with input `2010-01-01` would give `2016-01-01 10:00:00`. `From` can be a date, but you can also be more specific and supply a time, like `from 2016-01-01 15:30` 

### Until
This constraint is a little special. You use it just like you use `From`. `every day until 2020-01-01`. So what will happen the day we pass in a date higher than `2020-01-01`? Well. There´s no sensible next occurence really, so we will throw an `InvalidOperationException`. Would we get an exception if we passed in `2010-01-01`? Remeber, constraints are inclusive, so no, we wouldn't.

## Fluent Api
In version 2.2.0, I added a fluent version of the api. It is available as a separate nuget package. You can explore it yourself by start typing any of these lines:  
```csharp

using GoldenFox.Fluent

Every. // use intellisense from here
1.St(). // use intellisense from here
First(). // use intellisense from here
2.Nd(). // use intellisense from here
Last(). // use intellisense from here

```

## Contribute

Besides being used in production code in some of my projects, this is my pet project for learning how to create DSLs. All ideas on new features and how to improve the code are welcome.

