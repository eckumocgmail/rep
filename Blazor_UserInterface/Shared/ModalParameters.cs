public class ModalParameters
{
    public class ModalParameter
    {
        public string name;
        public object value;
        public bool required;
        public Func<object, bool> validate;
        public string type;
        public Action onChange;
    }

    List<ModalParameter> parameters = new();
  

    public void Add(string name, object value)
    {
        Add(name, value, true, (val) => true, "text");
    }
    public void Add(string name, object value, bool required, Func<object,bool> validate, string type)
    {
        parameters.Add(new ModalParameter { name=name, value=value, required=required, validate=validate, type=type });
    }
}