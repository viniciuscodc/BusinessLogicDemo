import smtplib
from email.mime.multipart import MIMEMultipart
from email.mime.base import MIMEBase
from email.mime.text import MIMEText
from email.utils import formatdate
from email import encoders
import os,datetime
import sys
import locale 

locale.setlocale(locale.LC_ALL, 'pt_BR.UTF-8')

# py main.py  '26/01/2022 10:52:00' 4 'texto texto texto' 'Assunto texto texto' vinicius.costa@organnact.com.br
def sendInvitation(start, duration, email_content, subject, attendees):

    CRLF = '\r\n'

    login = 'organnact.sistemas@outlook.com'
    password = 'Orga@2021'
    organizer = 'ORGANIZER;CN=Test:mailto:email'+CRLF+'@gmail.com'
    _from = 'nickname <email@outlook.com>'

    start_day, start_month, start_year = start.split('/')

    start_year, start_time = start_year.split(' ')

    start_hour, start_minute, start_second = start_time.split(':')

    timezone = datetime.timedelta(hours = 3)
    date_start = datetime.datetime(int(start_year), int(start_month), int(start_day), int(start_hour), int(start_minute))
    duration = datetime.timedelta(minutes= int(duration))
    date_end = date_start + duration

    date_stamp = datetime.datetime.now().strftime('%Y%m%dT%H%M%SZ')
    date_start_str = date_start.strftime('%Y%m%dT%H%M%SZ')
    date_end = date_end.strftime('%Y%m%dT%H%M%SZ')

    date_start_tm = date_start - timezone

    description = 'DESCRIPTION: test invitation from pyICSParser'+CRLF
    attendee = ''
    for att in attendees:
        attendee += 'ATTENDEE;CUTYPE=INDIVIDUAL;ROLE=REQ-    PARTICIPANT;PARTSTAT=ACCEPTED;RSVP=TRUE'+CRLF+' ;CN='+att+';X-NUM-GUESTS=0:'+CRLF+' mailto:'+att+CRLF
    ical = 'BEGIN:VCALENDAR'+CRLF+'PRODID:pyICSParser'+CRLF+'VERSION:2.0'+CRLF+'CALSCALE:GREGORIAN'+CRLF+'METHOD:REQUEST'+CRLF 
    ical+='BEGIN:VEVENT'+CRLF+'DTSTART:'+date_start_str+CRLF+'DTEND:'+date_end+CRLF+'DTSTAMP:'+date_stamp+CRLF+organizer+CRLF
    ical+= 'UID:FIXMEUID'+date_stamp+CRLF
    ical+= attendee+'CREATED:'+date_stamp+CRLF+description+'LAST-MODIFIED:'+date_stamp+CRLF+'LOCATION:'+CRLF+'SEQUENCE:0'+CRLF+'STATUS:CONFIRMED'+CRLF
    ical+= 'SUMMARY:'+ subject + CRLF+'TRANSP:OPAQUE'+CRLF+'END:VEVENT'+CRLF+'END:VCALENDAR'+CRLF

    eml_body = email_content
    msg = MIMEMultipart('mixed')
    msg['Reply-To']='organnactcidteste@outlook.com'
    msg['Date'] = formatdate(localtime=True)
    msg['Subject'] = 'Assunto' + date_start_str
    msg['From'] = _from
    msg['To'] = ','.join(attendees)

    part_email = MIMEText(eml_body,'html')
    part_cal = MIMEText(ical,'calendar;method=REQUEST')

    msgAlternative = MIMEMultipart('alternative')
    msg.attach(msgAlternative)

    ical_atch = MIMEBase('application/ics',' ;name="%s"'%("invite.ics"))
    ical_atch.set_payload(ical)
    encoders.encode_base64(ical_atch)
    ical_atch.add_header('Content-Disposition', 'attachment; filename="%s"'%("invite.ics"))

    eml_atch = MIMEText('', 'plain')
    encoders.encode_base64(eml_atch)
    eml_atch.add_header('Content-Transfer-Encoding', "")

    msgAlternative.attach(part_email)
    msgAlternative.attach(part_cal)

    try:
        mailServer = smtplib.SMTP('smtp.outlook.com', 587)
        mailServer.ehlo()
        mailServer.starttls()
        mailServer.ehlo()
        mailServer.login(login, password)
        mailServer.sendmail(_from, attendees, msg.as_string())
        mailServer.close()

        print('Success')
    except:
        print('Fail')

start_arg = sys.argv[1]
duration_arg = sys.argv[2]
content_arg = sys.argv[3]
subject_arg = sys.argv[4]
attendees_args = sys.argv[5:]

sendInvitation(start_arg, duration_arg, content_arg, subject_arg, attendees_args)
