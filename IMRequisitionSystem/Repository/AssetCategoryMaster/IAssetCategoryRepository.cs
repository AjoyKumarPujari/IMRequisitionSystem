using IMRequisitionSystem.Models.Assets;
using IMRequisitionSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMRequisitionSystem.Repository
{
    public interface IAssetCategoryRepository
    {
        SPOutputMessage InsertAssetCategoryMaster(AssetCategoryModel assetsCategoryModel);
        List<AssetCategoryModel> GetAllAssetCategoryMaster();
        List<AssetCategoryModel> GetAllAssetCategoryForDropDown();

        SPOutputMessage UpdateActiveStatus(AssetCategoryModel assetsCategoryModel);
        
        
    }
}
