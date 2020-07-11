using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable, InlineProperty]
public class Stat
{
	[SerializeField, HideLabel]
	private int _baseValue = 100;
	[SerializeField, ReadOnly, HideInEditorMode]
	private int _modifiersSum;
	[SerializeField, ReadOnly, HideInEditorMode]
	private readonly List<StatModifier> modifiers = new List<StatModifier>();

	public int BaseValue 
	{ 
		get => _baseValue;
		private set
		{
			if (_baseValue != value)
			{
				_baseValue = value;
				onValueChange?.Invoke(FinalValue);
			}
		}
	}	
	public int FinalValue { get => _baseValue + _modifiersSum; }

	public delegate void OnValueChange(float newValue);
	public event OnValueChange onValueChange;

	public void AddModifier(StatModifier modifier)
	{
		modifiers.Add(modifier);
		_modifiersSum += modifier.value;
		onValueChange?.Invoke(FinalValue);
	}

	public void RemoveModifier(StatModifier modifier)
	{
		modifiers.Remove(modifier);
		_modifiersSum -= modifier.value;
		onValueChange?.Invoke(FinalValue);
	}

}