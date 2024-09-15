namespace Console_InputApplication
{

    /// <summary>
    /// Поставщик константных значений
    /// </summary>
    public class ValuesProvider
    {
        /// <summary>
        /// Имя абстрактного типа - надпись с наименование на русском языке
        /// </summary>
        /// <returns></returns>
        public Dictionary<string, string> GetEntityTypes() => new Dictionary<string, string>(
            ServiceFactory.Get().GetTypesExtended<BaseEntity>().Where(t => t == typeof(BaseEntity)).Select(p => p.GetTypeName())
                .Select( typeName => new KeyValuePair<string, string>(typeName, typeName.ToType().GetLabel())));

    
    }
}
