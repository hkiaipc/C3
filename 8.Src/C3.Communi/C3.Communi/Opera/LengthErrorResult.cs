
using System;
using Xdgk.Common;

namespace C3.Communi
{
    public class LengthErrorResult : ParseResultBase
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="expected"></param>
        /// <param name="actual"></param>
        public LengthErrorResult(int expected, int actual)
        {
            this._expected = expected;
            this._actual = actual;
        }

        /// <summary>
        /// ��ȡ�����ĳ���
        /// </summary>
        public int Expected
        {
            get { return _expected; }
        } private int _expected;

        /// <summary>
        /// ��ȡʵ��ֵ
        /// </summary>
        public int Actual
        {
            get { return _actual; }
        } private int _actual;

        public override string ToString()
        {
            string s = string.Format("{0}, {1} '{2}', {3} '{4}'", strings.LengthErrorResult,
                    strings.Expected, Expected, strings.Actual, Actual);
            return s;
        }
    }

}
