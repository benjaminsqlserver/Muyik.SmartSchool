using Muyik.SmartSchool.Samples;
using Xunit;

namespace Muyik.SmartSchool.EntityFrameworkCore.Domains;

[Collection(SmartSchoolTestConsts.CollectionDefinitionName)]
public class EfCoreSampleDomainTests : SampleDomainTests<SmartSchoolEntityFrameworkCoreTestModule>
{

}
