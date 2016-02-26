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

everyday: 'every day' At times;
everyminute: 'every minute' (At secondsOffset)? (between)?; 
everyhour: 'every hour' (At minutesOffset)? (between)?;
everysecond: 'every second' (between)?;
everyweekday: 'every' weekday At times;
weekdays: weekday's' At times;
numberedweekday: ((numberedDay (Last)?) | Last) 'day every week' At times;
numbereddayinmonth: ((numberedDay (Last)?) | Last) 'day every month' At times;

secondsOffset: ((('mm:'|'hh:mm:')INT) | (INT 'seconds')) ('and' secondsOffset)?;
minutesOffset: ((('hh:')INT(':'INT)?) | (INT 'minutes')) ('and' minutesOffset)?;
between: 'between' time 'and' time;
time: (INT':'INT(':'INT)?);
times: time ('and' time)?;
weekday: ('monday' | 'tuesday' | 'wednesday' | 'thursday' | 'friday' | 'saturday' | 'sunday');
numberedDay: INT('st'|'nd'|'rd'|'th');
 
WS: (' ' | '\t' | ('\r'? '\n'))+ -> channel(HIDDEN);
INT:[0-9]+;
At: ('at' | '@');
Last: 'last';