using System;

namespace C3.Data
{
    public class UnitType
    {
        private UnitType(string name)
            : this(name, name)
        {
        }
        private UnitType(string name, string text)
        {
            this.Name = name;
            this.Text = text;
        }

        #region Name
        /// <summary>
        /// 
        /// </summary>
        public string Name
        {
            get
            {
                if (_name == null)
                {
                    _name = string.Empty;
                }
                return _name;
            }
            set
            {
                if (value == null)
                    throw new ArgumentNullException("name");

                _name = value;
            }
        } private string _name;
        #endregion //Name

        #region Text
        /// <summary>
        /// 
        /// </summary>
        public string Text
        {
            get
            {
                if (_text == null)
                {
                    _text = string.Empty;
                }
                return _text;
            }
            set { _text = value; }
        } private string _text;
        #endregion //Text


        // 2012-07-23 unit type text
        //
        static public readonly UnitType
            None = new UnitType(""),
                 Length = new UnitType("Length", "����"),
                 Press = new UnitType("Press", "ѹ��"),
                 Temperature = new UnitType("Temperature", "�¶�"),
                 Flow = new UnitType("Flow", "����"),
                 Volume = new UnitType("Volume", "���"),
                 Time = new UnitType("Time", "ʱ��");
    }

}
