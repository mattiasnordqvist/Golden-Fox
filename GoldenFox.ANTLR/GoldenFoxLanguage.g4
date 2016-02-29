grammar GoldenFoxLanguage;

schedule: ( everyminute
          | everyhour
          | everysecond
          | everyday
          | everyweekday 
          | weekdays
          | numberedweekday
          | numbereddayinmonth
          | numbereddayinyear
          ) 
          ('and' schedule)?;

everyday: 'every day' At times from? until?;
everyminute: 'every minute' (At secondsOffset)? (between)? from? until?; 
everyhour: 'every hour' (At minutesOffset)? (between)? from? until?;
everysecond: 'every second' (between)? from? until?;
everyweekday: 'every' weekday At times from? until?;
weekdays: weekday's' At times from? until?;
numberedweekday: ((numberedDay (Last)?) | Last) 'day every week' At times from? until?;
numbereddayinmonth: ((numberedDay (Last)?) | Last) 'day every month' At times from? until?;
numbereddayinyear: ((numberedDay (Last)?) | Last) 'day every year' At times from? until?;

secondsOffset: ((('mm:'|'hh:mm:')INT) | (INT 'seconds')) ('and' secondsOffset)?;
minutesOffset: ((('hh:')INT(':'INT)?) | (INT 'minutes')) ('and' minutesOffset)?;
between: 'between' time 'and' time;
from: 'from' datetime;
until: 'until' datetime;
time: (INT':'INT(':'INT)?);
times: time ('and' time)?;
weekday: ('monday' | 'tuesday' | 'wednesday' | 'thursday' | 'friday' | 'saturday' | 'sunday');
numberedDay: INT('st'|'nd'|'rd'|'th');
datetime: (date)(time)?; 
date: INT'-'INT'-'INT;
 
WS: (' ' | '\t' | ('\r'? '\n'))+ -> channel(HIDDEN);
INT:[0-9]+;
At: ('at' | '@');
Last: 'last';