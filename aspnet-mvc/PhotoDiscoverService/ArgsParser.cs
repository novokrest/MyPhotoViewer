using System.Linq;

namespace PhotoDiscoverService
{
    internal class ArgsParser
    {
        private const string UpdateOnlyUsersArg = "--only-users";
        private const string UpdateOnlyPhotosArg = "--only-photos";

        private readonly string[] _args;

        public ArgsParser(string[] args)
        {
            _args = args;
        }

        public bool UpdateOnlyUsers => IsArgSpecified(UpdateOnlyUsersArg);

        public bool UpdateOnlyPhotos => IsArgSpecified(UpdateOnlyPhotosArg);

        private bool IsArgSpecified(string soughtForArg)
        {
            return _args.Any(arg => string.Equals(arg, soughtForArg));
        }
    }
}
