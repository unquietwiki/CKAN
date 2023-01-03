using System.Collections.Generic;
using System.IO;
using CKAN.NetKAN.Model;

namespace CKAN.NetKAN.Validators
{
    internal sealed class NetkanValidator : IValidator
    {
        private readonly List<IValidator> _validators;

        public NetkanValidator()
        {
            _validators = new List<IValidator>
            {
                new SpecVersionFormatValidator(),
                new HasIdentifierValidator(),
                new KrefValidator(),
                new AlphaNumericIdentifierValidator(),
                new RelationshipsValidator(),
                new LicensesValidator(),
                new KrefDownloadMutexValidator(),
                new DownloadVersionValidator(),
                new OverrideValidator(),
                new VersionStrictValidator(),
                new ReplacedByValidator(),
                new InstallValidator(),
            };
        }

        public void Validate(Metadata metadata)
        {
            foreach (var validator in _validators)
            {
                validator.Validate(metadata);
            }
        }

        public void ValidateNetkan(Metadata metadata, string filename)
        {
            Validate(metadata);
            new MatchingIdentifiersValidator(Path.GetFileNameWithoutExtension(filename)).Validate(metadata);
        }
    }
}
