grammar GoldenFoxLanguage;

schedule: interval (And schedule)?;
interval: Every (weekday | day);
weekday: Weekday At Time;
day: Day At Time;


WS: (' ' | '\t' | ('\r'? '\n'))+ -> channel(HIDDEN);
Every: 'every';
Day: 'day';
Weekday: ('monday' | 'tuesday' | 'wednesday' | 'thursday' | 'friday' | 'saturday' | 'sunday');
At: ('at' | '@');
Digit: [0-9];
And: 'and';
Time: (Hour':'Minute);
Hour: Digit Digit;
Minute: Digit Digit;