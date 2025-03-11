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
        List<AssetsModel> GetAllAllocateAssetMaster();

        SPOutputMessage UpdateActiveDeActiveAssetsStatus(AssetsModel assetsModel);
        
        
        List<AssetsModel> GetHealthCheckupDueAsset();
        List<AssetsModel> GetHealthCheckupHistory();


        SPOutputMessage UpdateActiveDeActiveAsCreateHealthCheckUpUpdatesetsStatus(AssetsModel assetsModel);
    }
}
