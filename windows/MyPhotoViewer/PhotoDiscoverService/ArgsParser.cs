using System.Linq;

namespace PhotoDiscoverService
{
    internal class ArgsParser
    {
        private const string UpdateOnlyUserArg = "--only-users";

        private readonly string[] _args;
        private bool? _updateOnlyUsers;

        public ArgsParser(string[] args)
        {
            _args = args;
        }

        public bool UpdateOnlyUsers
        {
            get { return _updateOnlyUsers ?? 
                        (_updateOnlyUsers = _args.Any(arg => string.Equals(arg, UpdateOnlyUserArg))).Value; }
        }
    }
}
