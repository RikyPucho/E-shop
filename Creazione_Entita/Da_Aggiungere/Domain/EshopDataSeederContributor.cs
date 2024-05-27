using Eshop.Prodotti;



{
    private readonly IProdottoRepository _prodottoRepository;
    private readonly ProdottoManager _prodottoManager;


    (
        IProdottoRepository prodottoRepository
        ProdottoManager prodottoManager
    )
    {
        _prodottoRepository = prodottoRepository
        _prodottoManager = prodottoManager
    }

    {
            if(await _prodottoRepository.GetCountAsync() <= 0)
            {
                await _prodottoRepository.insertAsync(
                    await _prodottoManager.CreateAsync(
                        //inserisci i dati
                    )
                );
                await _prodottoRepository.insertAsync(
                    await _prodottoManager