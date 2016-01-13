
public abstract class AbstractValueObject {

	protected object rawData { get; set; }

	public abstract AbstractValueObject clone ();
}
