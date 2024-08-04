
using System;
/// <summary>
/// Справочник ( дополнительная информация, не обрабатываеся приложением )
/// </summary>
public class EventsTable : NamedObject
{
    public DateTime Created;

}
public class EventsTable<T>: EventsTable
{
      
}