namespace Util
{
    public abstract class Transformer<TA, TB>
    {
        protected TA BeTransformed { get; }

        protected Transformer(TA beTransformed)
        {
            BeTransformed = beTransformed;
        }

        public abstract TB Transform();
    }
}