using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Console_InputApplication.ViewEvents
{
    /// <summary>
    /// Предполагается что это сообщение проходит по всей иерархии
    /// в 2 этапа:
    /// 1) движение вверх к корневому узлу
    /// 2) регистрация сообщение при движении вниз
    /// </summary>
    public class TransactEventMessage<T> :
                    CommonEventMessage<T> where T :
                        BaseEventMessage
    {
        private bool Active = false;

        public TransactEventMessage(T item) : base(item)
        {
        }

        public void Activate()
        {
            Active = true;
        }
        public bool IsActive()
        {
            return Active;
        }
    }
}
