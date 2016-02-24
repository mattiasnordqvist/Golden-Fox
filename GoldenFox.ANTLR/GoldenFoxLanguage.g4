grammar GoldenFoxLanguage;

schedule: interval (And schedule)?;
interval: 
            (Every (weekday | day)) 
          | numberedweekday
          | weekdays
          | numbereddayinmonth
          ;

weekday: Weekday At Time;
weekdays: Weekday's' At Time;
numberedweekday: ((NumberedDay (Last)?) | Last) Day Every Week At Time;
numbereddayinmonth: ((NumberedDay (Last)?) | Last) Day Every Month At Time;
day: Day At Time;


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
Time: (Hour':'Minute);
Hour: Digit Digit;
Minute: Digit Digit;