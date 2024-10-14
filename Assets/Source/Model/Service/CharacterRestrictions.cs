using UnityEngine;

public class CharacterRestrictions : ICharacterRestrictions
{
    private readonly Transform _restrictionsLeft;
    private readonly Transform _restrictionsRight;

    public CharacterRestrictions(Transform restrictionsLeft, Transform restrictionsRight)
    {
        _restrictionsLeft = restrictionsLeft;
        _restrictionsRight = restrictionsRight;
    }

    public float Left => _restrictionsLeft.position.x;
    public float Right => _restrictionsRight.position.x;
}