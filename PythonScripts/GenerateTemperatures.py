import math
import datetime
import random
import csv
import holidays
import pandas as pd

class TempHumModel:

    def __init__(self, temp, hum, date: datetime) -> None:
        self.Temperature = temp
        self.Humidity = hum
        self.Date = date

    def ToTuple(self):
        return (self.Date, self.Temperature, self.Humidity)


def GenerateTemperatures():
    pl_holidays = holidays.PL()
    date = datetime.datetime(2020, 1, 1)
    retList = []
    while date < datetime.datetime.now():
        date = date + datetime.timedelta(days=1)
        weekday = date.weekday()

        if IsSummerTime(date):
            if date.strftime("%Y-%m-%d") in pl_holidays or weekday is 6:
                temp, hum = GetTempAndHumForHolidaySummer()
            else:
                temp, hum = GetTempAndHumForWorkingDaysSummer()
        else:
            if date.strftime("%Y-%m-%d") in pl_holidays or weekday is 6:
                temp, hum = GetTempAndHumForHolidayWinter()
            else:
                temp, hum = GetTempAndHumForWorkingDaysWinter()

        retList.append(TempHumModel(temp, hum, date))
    
    return retList

def IsSummerTime(date):
    startDate = datetime.datetime(month=5, day=1, year=date.year)
    endDate = datetime.datetime(month=10, day=1, year=date.year)
    if date > startDate and date < endDate:
        return True
    
    return False

def GetTempAndHumForHolidayWinter():
        temp = random.randrange(18, 19) + round(random.random(), 1) + 0.5
        hum = random.randint(45, 60)

        return (temp, hum)

def GetTempAndHumForHolidaySummer():
        temp = random.randrange(20, 24) + round(random.random(), 1)
        hum = random.randint(40, 50)

        return (temp, hum)

def GetTempAndHumForWorkingDaysWinter():
        temp = random.randrange(18, 21) + round(random.random(), 1) + 0.5
        hum = random.randint(45, 65)

        return (temp, hum)

def GetTempAndHumForWorkingDaysSummer():
        temp = random.randrange(21, 24) + round(random.random(), 1)
        hum = random.randint(40, 53)

        return (temp, hum)


def WriteToFile(filePath, dataToWrite):
        try:
            with open(filePath, 'w', newline='') as f:
                writer = csv.writer(f)
                for item in dataToWrite:
                    writer.writerow(item.ToTuple())
        except BaseException as e:
            print('BaseException:', filePath)
        else:
            print('Data has been loaded successfully !')


def GroupDataByMonths(data: list[TempHumModel]):
    
    lastMonth = data[0].Date.month
    retList = []
    group = []
    for element in data:
        if element.Date.month != lastMonth:
            lastMonth = element.Date.month
            retList.append(group)
            group = []
        
        group.append(element)

    return retList

def WriteToExcel(data, filePath):

    df_list = []
    for list in data:
        temp = [x.ToTuple() for x in list]
        df = pd.DataFrame(temp, columns=["data", "temperatura", "wilgotność"])
        df_list.append((df, temp[0][0]))
        print(df)

    with pd.ExcelWriter(filePath) as writer:
        for df in df_list:
            name = df[1].strftime("%Y-%m")
            df[0].to_excel(writer, sheet_name=name)

    return df_list



if __name__ == "__main__":
    temperatures = GenerateTemperatures()
    grouped = GroupDataByMonths(temperatures)

    filePath = "C:\!Personal\PrivateRepo\PythonScripts\TestTemp.xlsx"
    WriteToExcel(grouped, filePath)

    filePath = "C:\!Personal\PrivateRepo\PythonScripts\TestTemp.csv"
    WriteToFile(filePath, temperatures)

