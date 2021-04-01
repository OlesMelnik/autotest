myApp = App("\"C:\\WINDOWS\\system32\\notepad.exe\"");
myApp.toString();
myApp.open();
myApp.focus();"1616702725886.png"
#Settings.MoveMouseDelay = 0
print(myApp.getName());
click("1616698463413.png");
click("1616702263367.png");
click("1616702340289.png");
type("C:\Users\olesm\Desktop\mytest.txt");
click("1616702451683.png");
waitVanish("1616702750689.png");
type("pizza time ");

click("1616698463413.png");
click("1616703439972.png");
click("1616703602433.png");
type("a", KeyModifier.CTRL) # select all text

type(Key.BACKSPACE) # delete selection

type("C:\Users\olesm\Desktop\mytest2.txt");
click("1616703536949.png");
if exists("1616704500153.png"):
    click("1616704516290.png");
print("done");