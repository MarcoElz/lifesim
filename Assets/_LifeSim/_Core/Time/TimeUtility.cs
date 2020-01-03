using System;

public static class TimeUtility 
{

    public static string GetSpanishMonth(int number)
    {
        return ((MonthSpanish)number).ToString();
    }

    public static string GetSpanishDayWeek(DayOfWeek dayOfWeek)
    {
        return ((DayOfWeekSpanish)((int)dayOfWeek)).ToString();
    }
}

public enum MonthSpanish { Enero = 1, Febrero = 2, Marzo = 3, Abril = 4, Mayo = 5, Junio = 6, Julio = 7, Agosto = 8, Septiembre = 9, Octubre = 10, Noviembre = 11, Diciembre = 12 }
public enum DayOfWeekSpanish
{
    Domingo = 0,
    Lunes = 1,
    Martes = 2,
    Miercoles = 3,
    Jueves = 4,
    Viernes = 5,
    Sabado = 6
}
