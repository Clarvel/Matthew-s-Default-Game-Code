namespace SoulFlare {
	/// <summary>
	/// Unsigned keeps the Anle in range 0 -> 360
	/// signed keeps the nalge in range -180 -> 180
	/// </summary>
	public enum AngleType { UNSIGNED, SIGNED }

	/// <summary>
	/// Helper class to contorl angles to facilitate working with eulerAngles
	/// </summary>
	public class Angle {
		private AngleType type;
		private float angle;

		public AngleType Type {
			get { return type; }
			set {
				if(type == AngleType.SIGNED && value == AngleType.UNSIGNED) {
					if(angle < 0f) {
						angle += 360;
					}
				} else if(type == AngleType.UNSIGNED && value == AngleType.SIGNED) {
					if(angle > 180f) {
						angle -= 360f;
					}
				} else {
					throw new System.NotImplementedException();
				}
				type = value;
			}
		}
		public float Value {
			get { return angle; }
			set {
				if(type == AngleType.SIGNED) {
					float a = (value + 180f) % 360f;
					angle = (a < 0f ? a + 360f : a) - 180f;
				} else if(type == AngleType.UNSIGNED) {
					float a = value % 360f;
					angle = a < 0f ? a + 360f : a;
				} else {
					throw new System.NotImplementedException();
				}
			}
		}

		public Angle() { }

		public Angle(float angle, AngleType type = AngleType.UNSIGNED) {
			this.type = type;
			Value = angle;
		}

		public static Angle operator +(Angle a, Angle b) {
			if(a.Type != b.Type) {
				throw new System.ArgumentException("Angles not of the same AngleType");
			}
			return new Angle(a.Value + b.Value, a.Type);
		}

		public static Angle operator -(Angle a, Angle b) {
			if(a.Type != b.Type) {
				throw new System.ArgumentException("Angles not of the same AngleType");
			}
			return new Angle(a.Value - b.Value, a.Type);
		}

		public Angle Reverse() {
			return new Angle(Value + 180f, Type);
		}

		public override string ToString() {
			if(type == AngleType.UNSIGNED) {
				return "U>" + angle;
			}
			return ">" + angle;
		}

		public float ToRad() {
			return Value * 0.0124533f;
		}

		public static Angle FromRad(float rad, AngleType type = AngleType.UNSIGNED) {
			return new Angle(rad * 57.2958f, type);
		}
	}
}