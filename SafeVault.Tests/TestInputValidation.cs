using NUnit.Framework;
using SafeVault.Helpers;

namespace SafeVault.Tests
{
    [TestFixture]
    public class TestInputValidation
    {
        // -------------------------
        // USERNAME VALIDATION TESTS
        // -------------------------

        [Test]
        public void Username_Should_Fail_When_Empty()
        {
            bool result = ValidationHelpers.ValidateUsername("", out string error);

            Assert.That(result, Is.False);
            Assert.That(error, Is.EqualTo("Username cannot be empty."));
        }

        [Test]
        public void Username_Should_Fail_When_TooShort()
        {
            bool result = ValidationHelpers.ValidateUsername("ab", out string error);

            Assert.That(result, Is.False);
            Assert.That(error, Is.EqualTo("Username must be between 3 and 50 characters."));
        }

        [Test]
        public void Username_Should_Fail_When_TooLong()
        {
            string longName = new string('a', 51);

            bool result = ValidationHelpers.ValidateUsername(longName, out string error);

            Assert.That(result, Is.False);
            Assert.That(error, Is.EqualTo("Username must be between 3 and 50 characters."));
        }

        [Test]
        public void Username_Should_Fail_When_Contains_DisallowedCharacters()
        {
            bool result = ValidationHelpers.ValidateUsername("User!123", out string error);

            Assert.That(result, Is.False);
            Assert.That(error, Is.EqualTo("Username can only contain letters, numbers, @, #, $."));
        }

        [Test]
        public void Username_Should_Pass_With_AllowedCharacters()
        {
            bool result = ValidationHelpers.ValidateUsername("User123@#$", out string error);

            Assert.That(result, Is.True);
            Assert.That(error, Is.EqualTo(""));
        }

        // -------------------------
        // PASSWORD VALIDATION TESTS
        // -------------------------

        [Test]
        public void Password_Should_Fail_When_Empty()
        {
            bool result = ValidationHelpers.ValidatePassword("", out string error);

            Assert.That(result, Is.False);
            Assert.That(error, Is.EqualTo("Password cannot be empty or whitespace."));
        }

        [Test]
        public void Password_Should_Fail_When_TooShort()
        {
            bool result = ValidationHelpers.ValidatePassword("1234567", out string error);

            Assert.That(result, Is.False);
            Assert.That(error, Is.EqualTo("Password must be at least 8 characters long."));
        }

        [Test]
        public void Password_Should_Fail_When_TooLong()
        {
            string longPassword = new string('a', 129);

            bool result = ValidationHelpers.ValidatePassword(longPassword, out string error);

            Assert.That(result, Is.False);
            Assert.That(error, Is.EqualTo("Password must be shorter than 128 characters."));
        }

        [Test]
        public void Password_Should_Pass_With_ValidLength()
        {
            bool result = ValidationHelpers.ValidatePassword("ValidPass123", out string error);

            Assert.That(result, Is.True);
            Assert.That(error, Is.EqualTo(""));
        }
    }
}