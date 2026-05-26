using DevFreela.UnitTests.Common.Helpers;

namespace DevFreela.UnitTests.Application.Features.Users.PutProfilePicture
{
    public class PutProfilePictureCommandTests
    {
        [Fact]
        public void InputDataIsValid_PutProfilePicture_Success()
        {
            var command = FakeDataHelper.CreateFakePutProfilePictureCommand();
            Assert.NotNull(command);
        }
    }
}
