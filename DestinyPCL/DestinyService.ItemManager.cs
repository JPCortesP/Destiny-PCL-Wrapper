using DestinyPCL.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DestinyPCL
{
    public partial class DestinyService : IDestinyService
    {
        public Task<bool> EquipItem(BungieUser AuthUser, DestinyCharacter target, DestinyItemBase item)
        {
            if (!AuthHelpers.CheckForRequiredCookies(AuthUser.cookies))
                throw new NotSupportedException("You have to provide an Autenticated User, with correct Cookies");



            throw new NotImplementedException();
        }
        public Task<bool> TransferItem(BungieUser AuthUser, DestinyCharacter target, DestinyItemBase item, int quantity = 1, bool toVault = false)
        {
            if (!AuthHelpers.CheckForRequiredCookies(AuthUser.cookies))
                throw new NotSupportedException("You have to provide an Autenticated User, with correct Cookies");


            throw new NotImplementedException();
        }
    }
}
