FROM mono
RUN \
	apt-get update && \
	apt-get install -y mono-complete mono-xsp4 && \
	apt-get clean && \
	rm -rf /var/lib/apt/lists/* && \
	mkdir -p /var/www

ADD . /usr/src/CounterService
WORKDIR /usr/src/CounterService
RUN nuget restore -NonInteractive
RUN xbuild /property:Configuration=Release /property:OutDir=/var/www/
	
WORKDIR /var/www/_PublishedWebsites/CounterService
EXPOSE 80
	
CMD ["/bin/bash", "-c", "xsp4 --nonstop --port=80 --address 0.0.0.0 --verbose"]
