namespace project.Entities
{
    public class CustomerBasketDto
    {
        public CustomerBasketDto()
        {

        }
        public CustomerBasketDto(string id)
        {
            Id = id;
        }

        public string Id { get; set; }

        public List<BasketItemDto> list { get; set; } = new List<BasketItemDto>();
    }
}
