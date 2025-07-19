using Muyik.SmartSchool.Samples;
using Xunit;

namespace Muyik.SmartSchool.EntityFrameworkCore.Applications;

[Collection(SmartSchoolTestConsts.CollectionDefinitionName)]
public class EfCoreSampleAppServiceTests : SampleAppServiceTests<SmartSchoolEntityFrameworkCoreTestModule>
{

}
