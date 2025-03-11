using Dapper;
using Holiday_Home.Util;
using IMRequisitionSystem.Models.Assets;
using IMRequisitionSystem.Models;
using IMRequisitionSystem.Util;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMRequisitionSystem.Repository.Return
{
    public interface IReturnRepository
    {
        SPOutputMessage RequisitionReturn(RequisitionRequestModel requisitionRequestModel);
        SPOutputMessage RequisitionReturnAcknowledge(AssetsModel assetsModel);

        List<AssetsModel> GetAllReturnRequestedAssetMaster();
        List<AssetsModel> GetAllReturnReceivedAssetMaster();

    }
}
