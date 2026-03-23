.PHONY: run
FILE ?= file_name

run:
	@echo "(1) server"
	@echo "(2) producer"
	@echo "(3) producer-api"
	@echo "(4) consumer"
	@read -p "Please select 1-4: " app ; \
	case $$app in \
		1) docker compose up --build ;; \
		2) (cd src\Producer && dotnet run) ;; \
		3) (cd src\ProducerApi && dotnet run) ;; \
		4) (cd src\Consumer && dotnet run) ;; \
		*) echo "Invalid option" ;; \
	esac
