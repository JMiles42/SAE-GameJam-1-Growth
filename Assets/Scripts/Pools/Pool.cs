using System;
using System.Collections.Generic;
using ForestOfChaosLib.Attributes;
using ForestOfChaosLib.Extensions;
using UnityEngine;
using Object = UnityEngine.Object;

	public abstract class Pool: ScriptableObject
	{
		[SerializeField] [DisableEditing]        private int       currentIndex;
		[SerializeField] [DisableEditing]        private int       currentLength;
		[SerializeField]                         private bool      expandable;
		[SerializeField]                         private bool      forceReuse = true;
		[GetSetter("MaxCount")] [SerializeField] private int       maxCount   = 10;
		private                                          Transform transform;

		public int MaxCount
		{
			get { return maxCount; }
			set
			{
#if UNITY_EDITOR
				if(Application.isPlaying)
					ChangePoolSize(maxCount - value);
#else
				ChangePoolSize(maxCount - value);
				#endif
				maxCount = currentLength = value;
			}
		}

		public bool Expandable
		{
			get { return expandable; }
			set { expandable = value; }
		}

		public bool ForceReuse
		{
			get { return forceReuse; }
			set { forceReuse = value; }
		}

		public int CurrentLength
		{
			get { return currentLength; }
			protected set { currentLength = value; }
		}

		public int CurrentIndex
		{
			get { return currentIndex; }
			protected set { currentIndex = value; }
		}

		public abstract object NextRaw    { get; }
		public abstract object CurrentRaw { get; }

		public Transform Transform => transform? transform : CreateParentTransform();
		public abstract void ChangePoolSize(int newSize);

		private Transform CreateParentTransform()
		{
			var go = new GameObject(GetType().Name);
			go.ResetPosRotScale();
			go.transform.SetParent(PoolManager.Instance.transform);

			return transform = go.transform;
		}

		public abstract void InitPool();
	}

public abstract class Pool<T>: Pool where T: Object
{
	[SerializeField] [DisableEditing] private T         current;
	public                                    Action<T> OnReposition;
	public                                    T         PoolObject;

	public T Next => GetNext();

	/// <inheritdoc />
	public override object NextRaw => GetNext();

	public T Current
	{
		get { return current == null? current : (current = GetNext()); }
		private set { current = value; }
	}

	/// <inheritdoc />
	public override object CurrentRaw => Current;

	public List<T> Objects { get; private set; }

	public T RandomActive
	{
		get
		{
			var active = new List<T>();

			foreach(var o in Objects)
			{
				if(!IsUsable(o))
					active.Add(o);
			}

			return active.UnityRandomObject();
		}
	}

	public override void InitPool()
	{
		Objects = new List<T>(MaxCount);

		for(var i = 0; i < MaxCount; i++)
			InitSingleObject();

		CurrentLength = Objects.Count;
	}

	private T InitSingleObject()
	{
		var t = Instantiate();
		Objects.Add(t);

		return t;
	}

	private T GetNext()
	{
		for(var i = 0; i < Objects.Count; i++)
		{
			var t = Objects[(CurrentIndex + i + 1) % Objects.Count];

			if(IsUsable(t))
			{
				DoNextPreparation(t);
				CurrentIndex = i;

				return current = t;
			}
		}

		if(Expandable)
		{
			var t = InitSingleObject();
			DoNextPreparation(t);
			CurrentIndex  = Objects.Count - 1;
			CurrentLength = Objects.Count;

			return t;
		}

		if(ForceReuse)
		{
			var t = GetNextForced();
			DoNextPreparation(t);
			CurrentLength = Objects.Count;

			return t;
		}

		return null;
	}

	private T GetNextForced()
	{
		CurrentIndex = (CurrentIndex + 1) % CurrentLength;

		return Objects[CurrentIndex];
	}

	public override void ChangePoolSize(int newSize)
	{
		if(newSize > 0)
		{
			for(var i = 0; i < newSize; i++)
				InitSingleObject();
		}
		else if(newSize < 0)
		{
			for(var i = Objects.Count - 1; i >= 0; i--)
			{
				if((newSize != 0) && IsUsable(Objects[i]))
				{
					newSize++;
					DoDeletePreparation(Objects[i]);
					Objects.Remove(Objects[i]);
				}
			}
		}
	}

	protected abstract T Instantiate();
	protected abstract T DoNextPreparation(T   go);
	protected abstract T DoDeletePreparation(T go);
	protected abstract bool IsUsable(T         go);

	private void OnEnable()
	{
		CurrentIndex = 0;
		Current      = null;
	}

	private void OnDisable()
	{
		Current = null;
	}
}