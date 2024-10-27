using Microsoft.EntityFrameworkCore;

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Console_StatisticsModel.BusinessAnaliticsServices
{

    /**
     * 
     * create procedure insert_primary_data
( 
	@BusinessResourceID int,
	@BusinessDatasetID int,
	@BusinessIndicatorID int,
	@BeginDate datetime,
	@GranularityID int,
	@IndValue float
)
as
begin
	insert into BusinessData(BusinessIndicatorID, BusinessDatasetID, GranularityID, BusinessResourceID, BeginDate, IndValue, Changed)
	values( @BusinessIndicatorID, @BusinessDatasetID, @GranularityID, @BusinessResourceID, @BeginDate, @IndValue, GetDate() )

end

     */
    public class BusinessAnaliticsService
    {
        private readonly BusinessDataModel businessDataModel;


        public BusinessAnaliticsService(BusinessDataModel businessDataModel)
        {
            this.businessDataModel = businessDataModel;
        }

        public BusinessIndicator GetIndicatorByName(string name)
        {
            return this.businessDataModel.BusinessIndicators.FirstOrDefault(i => i.Name.ToLower() == name.ToLower());
        }

        public IEnumerable<BusinessDataset> GetDatasets() 
            => businessDataModel.BusinessDatasets.ToList();


        public Dictionary<string, List<float?>> GetLineSeriesData(int resourceId, int datasetId, string beginDate, int granularity)
        {
            Dictionary<string, List<float?>> result = new();
            
            var dataset = GetDatasetValues(resourceId,datasetId,beginDate,granularity);
            var indicator_ids = dataset.Select(p => p.BusinessIndicatorID).ToList();
            var indicators = businessDataModel.BusinessIndicators.Where( ind => indicator_ids.Contains(ind.Id)).Select( ind => new
            {
                Title = ind.Name,
                Subtitle = ind.Description,
                Id = ind.Id
            } ).ToList();

            foreach(var indicator in indicators)
            {
                result[indicator.Title] = dataset.Where(data => data.BusinessIndicatorID == indicator.Id).Select(data => data.IndValue).ToList();                
            }
            return result;
        }


        public Dictionary<string, IEnumerable<BusinessData>> GetBusinessResourcesData(int datasetId, string beginDate, int granularity)
        {
            Dictionary<string, IEnumerable<BusinessData>> result = new();
            foreach (var res in GetResources())
            {
                result[res.Id.ToString()] = GetDatasetValues(res.Id, datasetId, beginDate, granularity);
            }
            return result;
        }



        public IEnumerable<BusinessData> GetIndicatorValues(int indicatorId, int datasetId, string beginDate, int granularity)
        {
            var bdate = DateTime.ParseExact(beginDate, "yyyy-MM-dd", new CultureInfo("us"));
            return businessDataModel.BusinessData.Where(data => data.BusinessIndicatorID == indicatorId && data.BusinessDatasetID == datasetId && data.BeginDate == bdate && data.GranularityID == granularity);
        }
        public IEnumerable<BusinessData> GetDatasetValues(int resourceId, int datasetId, string beginDate, int granularity)
        {
            var bdate = DateTime.ParseExact(beginDate, "yyyy-MM-dd", new CultureInfo("us"));
            /*
                create procedure get_primary_data
                ( 
	                @BusinessResourceID int,
	                @BusinessDatasetID int, 
	                @BeginDate datetime,
	                @GranularityID int 
                )
                as
                begin
	                select * from BusinessData pdata 
	                where 
	                  pdata.BusinessResourceID=@BusinessResourceID and
	                  pdata.BusinessDatasetID=@BusinessDatasetID and
	                  pdata.BeginDate=@BeginDate and
	                  pdata.GranularityID=@GranularityID 
                end             
             */
            return businessDataModel.BusinessData.Where(data => data.BusinessResourceID == resourceId && data.BusinessDatasetID == datasetId && data.BeginDate == bdate && data.GranularityID == granularity);
        }


        public int AddData(int resourceId, int indicatorId, int datasetId, string beginDate, int granularity, float value)
        {
            Console.WriteLine($"exec insert_primary_data @BusinessResourceID={resourceId},@BusinessDatasetID={datasetId},@BusinessIndicatorID={indicatorId},@BeginDate={beginDate},@GranularityID={granularity},@IndValue={value}");
            var bd = DateTime.ParseExact(beginDate, "yyyy-MM-dd", new CultureInfo("us"));
            BusinessData newData = new()
            {
                BeginDate = bd,
                BusinessDatasetID = datasetId,
                GranularityID = granularity,
                BusinessIndicatorID = indicatorId,
                BusinessResourceID = resourceId,
                Changed = DateTime.Now,
                IndValue = value
            };
            newData.EnsureIsValide();
            businessDataModel.BusinessData.Add(newData);
            return businessDataModel.SaveChanges();
        }


        public BusinessDatasource AddDatasource( string name, string desription, string connectionString )
        {
            BusinessDatasource target = new()
            {
                Name = name,
                Description = desription,
                ConnectionString = connectionString
            };
            businessDataModel.BusinessDatasources.Add( target );
            businessDataModel.SaveChanges();
            return target;
        }


        public BusinessIndicator AddIndicator(string name, string desription)
        {
            BusinessIndicator target = new()
            {
                Name = name,
                Description = desription
            };
            businessDataModel.BusinessIndicators.Add(target);
            businessDataModel.SaveChanges();
            return target;
        }

        public string GetConnectionString(string name)
        {

            var datasource = businessDataModel.BusinessDatasources.FirstOrDefault(ds => ds.Name.ToLower() == name.ToLower());
            if(datasource is null)
            {
                throw new ArgumentException("name", "Значение в аргументе name не существующее");
            }
            return datasource.ConnectionString;
        }

        public IEnumerable<BusinessIndicator> GetIndicators()
        {
            return businessDataModel.BusinessIndicators.ToList();
        }

        

        public IEnumerable<BusinessResource> GetResources()
        {
            return businessDataModel.BusinessResources.ToList();
            
        }

        public BusinessDataset GetDatasetByName(string name)
        {
            var res = businessDataModel.BusinessDatasets.FirstOrDefault(ds => ds.Name.ToLower() == name.ToLower());
            if(res is null)
            {
                throw new ArgumentException("name", name+" не найден");
            }    
            return res;
        }
    }
}
