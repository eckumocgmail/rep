namespace pickpoint_delivery_service
{
    /// <summary>
    /// Функция публикации событий 
    /// </summary>
    public interface IEventsService
    {
        /// <summary>
        /// Публикация события
        /// </summary>
        public void Publish(string type, object data);
    }

}
