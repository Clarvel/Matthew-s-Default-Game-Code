public class Triple<T1, T2, T3> : Tuple<T1, T2> {
	public T3 Third;

	internal Triple() { }

	internal Triple(T1 first, T2 second, T3 third) : base(first, second) {
		Third = third;
	}
}
