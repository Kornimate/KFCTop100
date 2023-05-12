using AutoMapper;
using KFCSharedData;
using KFCSharedData.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace KFCTop100WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecordsApiController : ControllerBase
    {
        private readonly IKFCService service;
        private readonly IMapper mapper;

        public RecordsApiController(IMapper mapper, IKFCService service)
        {
            this.mapper = mapper;
            this.service = service;
        }

        [HttpPost]
        public ActionResult PostRecordsApi([FromBody] RecordDTO dto)
        {
            if(!ModelState.IsValid) return BadRequest(ModelState);
            try
            {
                string[] data = dto.Address!.Split('.');
                int num1 = Convert.ToInt32(data[1]);
                int num2 = BitConverter.ToInt32(Convert.FromBase64String(data[2]));
                if (num1 != num2) throw new Exception();
            }
            catch
            {
                return ValidationProblem();
            }
            Record newRec = (Record)dto;
            service.SaveRecordToDatabase(newRec);
            return Ok();
        }
    }
}
