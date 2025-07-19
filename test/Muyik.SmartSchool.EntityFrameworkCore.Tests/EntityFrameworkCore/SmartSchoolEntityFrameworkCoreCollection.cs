using Xunit;

namespace Muyik.SmartSchool.EntityFrameworkCore;

[CollectionDefinition(SmartSchoolTestConsts.CollectionDefinitionName)]
public class SmartSchoolEntityFrameworkCoreCollection : ICollectionFixture<SmartSchoolEntityFrameworkCoreFixture>
{

}
