using IMRequisitionSystem.Models;
using IMRequisitionSystem.Models.Assets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMRequisitionSystem.Repository
{
    public interface IAssetMasterRepository
    {
        SPOutputMessage InsertAssetMaster(AssetsModel assetsModel);
        List<AssetsModel> GetAllAssetMaster();


    }
}
