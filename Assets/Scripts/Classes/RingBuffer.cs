using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SoulFlare {
	/// <summary>
	/// Ring Buffer is a fixed-size array that you can freely push and pop into, and will overwrite old indexes should it loop around
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public class RingBuffer<T> {
		T[] array;
		int index;

		public uint Length { get; private set; }

		public RingBuffer(uint size) {
			array = new T[size];
		}

		public void Push(T obj) {
			array[index] = obj;
			index = (index + 1) % array.Length;
			if(Length < array.Length) {
				Length++;
			}
		}

		public T Pop() {
			if(Length < 1) {
				throw new System.IndexOutOfRangeException();
			}
			Length--;
			index = GetPreviousIndex();
			return array[index];
		}

		public T Peek() {
			if(Length < 1) {
				throw new System.IndexOutOfRangeException();
			}
			return array[GetPreviousIndex()];
		}

		public void Clear() {
			Length = 0;
		}

		int GetPreviousIndex() {
			return (index - 1 + array.Length) % array.Length;
		}
	}
}
