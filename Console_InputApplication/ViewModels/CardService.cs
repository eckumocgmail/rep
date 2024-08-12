using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
public class CardService
{
    public global::Card ForEntity(object record)
    {
        var card = new global::Card(record);

        return card;
    }
}