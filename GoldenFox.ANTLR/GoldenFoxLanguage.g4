grammar GoldenFoxLanguage;

schedule: ( everyminute
          | everyhour
          | everysecond
          | everyday
          | everyweekday 
          | weekdays
          | numberedweekday
          | numbereddayinmonth
          ) 
          ('and' schedule)?;

everyday: 'every day' At time;
everyminute: 'every minute' (At secondsOffset)? (between)?; 
everyhour: 'every hour' (At minutesOffset)? (between)?;
everysecond: 'every second' (between)?;
everyweekday: 'every' weekday At time;
weekdays: weekday's' At time;
numberedweekday: ((numberedDay (Last)?) | Last) 'day every week' At time;
numbereddayinmonth: ((numberedDay (Last)?) | Last) 'day every month' At time;

secondsOffset: ((('mm:'|'hh:mm:')INT) | (INT 'seconds')) ('and' secondsOffset)?;
minutesOffset: ((('hh:')INT(':'INT)?) | (INT 'minutes')) ('and' minutesOffset)?;
between: 'between' time 'and' time;
time: (INT':'INT(':'INT)?);
weekday: ('monday' | 'tuesday' | 'wednesday' | 'thursday' | 'friday' | 'saturday' | 'sunday');
numberedDay: INT('st'|'nd'|'rd'|'th');
 
WS: (' ' | '\t' | ('\r'? '\n'))+ -> channel(HIDDEN);
INT:[0-9]+;
At: ('at' | '@');
Last: 'last';