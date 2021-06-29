public class Tuple<T1, T2> {
	public T1 First;
	public T2 Second;

	internal Tuple() { }

	internal Tuple(T1 first, T2 second) {
		First = first;
		Second = second;
	}
}
