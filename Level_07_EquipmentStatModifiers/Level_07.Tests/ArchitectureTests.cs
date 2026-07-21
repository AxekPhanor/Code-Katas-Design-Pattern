using System.Reflection;

using Level07.Equipment.Tests.Reflection;

using Xunit;

namespace Level07.Equipment.Tests;

public sealed class ArchitectureTests
{
    private static readonly Assembly Sut = PatternInspector.SystemUnderTest;

    [Fact]
    public void Modifiers_wrap_the_equipment_they_enhance()
    {
        Assert.True(
            PatternInspector.HasDecorator(Sut, typeof(IEquipment)),
            "Un modificateur devrait ENVELOPPER un autre équipement (même contrat) " +
            "au lieu d'être un booléen dans la classe de base — c'est ce qui permet " +
            "d'empiler dynamiquement les effets.");
    }
}
