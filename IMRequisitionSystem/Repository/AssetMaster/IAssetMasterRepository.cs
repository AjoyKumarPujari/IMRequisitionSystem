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
        List<AssetsModel> GetAllModelNo();
        List<AssetsModel> GetAllLicenseNo();
        List<AssetsModel> GetAllAllocateAssetMaster();

        SPOutputMessage UpdateActiveDeActiveAssetsStatus(AssetsModel assetsModel);
       
        
        List<AssetsModel> GetHealthCheckupDueAsset();
        List<AssetsModel> GetHealthCheckupHistory();


        SPOutputMessage UpdateActiveDeActiveAsCreateHealthCheckUpUpdatesetsStatus(AssetsModel assetsModel);
      

        List<AssetsModel> GetAllIssuedAssetMaster();

        AssetModelForAPI GetDetailsByWorkOrderNoFromProduction(string id);
        AssetsModel GetAssetsDataBySAPID(string id);

        SPOutputMessage InsertAssetAMCMasterMapping(AssetsModel assetsModel);

        List<AssetsModel> GetAllAMCMappingAssetMaster();
       

        AssetsModel GetMappingAssetDataByID(string asset_Code_System);

        
        SPOutputMessage AMCMappingPageEditUpdate(AssetsModel assetsModel);

        List<AssetsModel> GetAllAMCMappingHistoryAssetMaster();
        List<AssetsModel> GetAMCMappingListWithFilterData(AssetsModel assetsModel);
        List<AssetsModel> GetMasterListWithFilterData(AssetsModel assetsModel);
        List<AssetsModel> GetAMCMappingHistoryListWithFilterData(AssetsModel assetsModel);
        List<AssetsModel> GetHealthCheckupListWithFilterData(AssetsModel assetsModel);

        SPOutputMessage UpdateActivePhysicalCondition(AssetsModel assetsModel);

    }
}
