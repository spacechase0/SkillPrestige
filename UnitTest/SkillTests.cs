﻿using System.Linq;
using Moq;
using NUnit.Framework;
using SkillPrestige;
using SkillPrestige.Mods;
using SkillPrestige.Mods.LuckSkill;
using SkillPrestige.SkillTypes;


namespace UnitTest
{
    [TestFixture]
    public class SkillTests
    {
        [OneTimeSetUp]
        public void Initialize()
        {
            var mod = new Mock<LuckSkillMod> { CallBase = true };
            mod.Setup(x => x.IsFound).Returns(true);
            ModHandler.RegisterMod(mod.Object);
        }


        [Test]
        public void SkillType_EqualsSameSkillType_ReturnsTrue()
        {
            var skillType = new SkillType();
            // ReSharper disable once EqualExpressionComparison - checking for equals comparison override.
            Assert.IsTrue(skillType.Equals(skillType));
            var skillType2 = skillType;
            Assert.IsTrue(skillType == skillType2);
        }

        [Test]
        public void SkillType_EqualsSameDefinedSkillType_ReturnsTrue()
        {
            var skillType = SkillType.Mining;
            Assert.IsTrue(skillType.Equals(SkillType.Mining));
            Assert.IsTrue(skillType == SkillType.Mining);
        }

        [Test]
        public void AllSkills_ContainsSkills_ReturnsTrue()
        {
            
            var skillType = SkillType.Mining;
            Assert.IsTrue(Skill.AllSkills.Select(x => x.Type).Contains(skillType));
            // ReSharper disable once ReturnValueOfPureMethodIsNotUsed - testing that the line does not throw an exception
            Assert.DoesNotThrow(delegate { Skill.AllSkills.Single(x => x.Type == skillType);} );
        }
    }
}
