using UnityEngine;

namespace PlayInThirdPerson
{
	public class Config
	{
		public virtual bool Enabled { get; set; } = true;
		public virtual Vector3 Offset { get; set; } = new Vector3(0f, 0f, -0.5f);
	}
}
