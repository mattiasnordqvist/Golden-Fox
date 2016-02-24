grammar GoldenFoxLanguage;

schedule: ( minute
          | hour
          | second
          | weekdays
          | weekday 
          | day
          | numberedweekday
          | numbereddayinmonth) 
          (And schedule)?;

weekday: Every Weekday At Time;
weekdays: Weekday's' At Time;
numberedweekday: ((NumberedDay (Last)?) | Last) Day Every Week At Time;
numbereddayinmonth: ((NumberedDay (Last)?) | Last) Day Every Month At Time;
day: Every Day At Time;
hour: Every 'hour' ('between' Time And Time)?;
minute: Every 'minute' ('between' Time And Time)?;
second: Every 'second' ('between' Time And Time)?;


WS: (' ' | '\t' | ('\r'? '\n'))+ -> channel(HIDDEN);
Every: 'every';
Day: 'day';
Week: 'week';
Month: 'month';
Weekday: ('monday' | 'tuesday' | 'wednesday' | 'thursday' | 'friday' | 'saturday' | 'sunday');
At: ('at' | '@');
Digit: [0-9];
Last: 'last';
And: 'and';
NumberedDay: (Digit Digit? ('st' | 'nd' | 'rd' | 'th'));
Time: (Hour':'Minute(':'Second)?);

Hour: Digit Digit;
Minute: Digit Digit;
Second: Digit Digit;