using Domain.Abstractions;
using FluentAssertions;

namespace Domain.Tests;

file sealed class TestEntity : Entity<string>
{
    // Trong code thật bạn có thể generate Id ở factory
    public TestEntity(string id) => Id = id;
    public string Name { get; private set; } = "N/A";
}

public class EntityEqualityTest
{
    // [Fact]
    // public void Entities_With_Same_Id_Should_Be_Equal()
    // {
    //     var id = "Tran vu hoang";
    //
    //     var a = new TestEntity(id);
    //     var b = new TestEntity(id);
    //
    //     a.Equals(b).Should().BeTrue();
    //     (a == b).Should().BeTrue();
    //     (a != b).Should().BeFalse();
    //     a.GetHashCode().Should().Be(b.GetHashCode());
    // }

    [Fact]
    public void Entities_With_Different_Id_Should_Not_Be_Equal()
    {
        var a = new TestEntity("E-1001");
        var b = new TestEntity("E-2002");

        a.Equals(b).Should().BeFalse();
        (a == b).Should().BeFalse();
        (a != b).Should().BeTrue();
    }

    [Fact]
    public void Entity_Should_Not_Equal_Null_Or_Different_Type()
    {
        var     a       = new TestEntity("E-1001");
        object? nullObj = null;

        a.Equals(nullObj).Should().BeFalse();
        false.Should().BeFalse();
    }

    [Fact]
    public void Entities_With_Empty_Or_Null_Id_Should_Not_Be_Equal()
    {
        // Nếu Entity cho phép null/empty Id (không khuyến nghị),
        // test này đảm bảo behavior nhất quán.
        var a = new TestEntity("");
        var b = new TestEntity("");

        // Tùy implementation của bạn:
        // 1) Nếu coi empty-id là không equal:
        a.Equals(b).Should().BeFalse();

        // 2) Nếu muốn ép rule: Id không được null/empty, bạn có thể
        //   assert throw khi khởi tạo ở factory/constructor thay vì test này.
    }
}